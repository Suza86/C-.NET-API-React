import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { Link, NavLink } from 'react-router-dom';

interface FetchHeistMemberDataState {
    heistMemberList: HeistMemberData[];
    loading: boolean;
}

export class FetchHeistMember extends React.Component<RouteComponentProps<{}>, FetchHeistMemberDataState> {
    constructor() {
        super();
        this.state = { heistMemberList: [], loading: true };

        fetch('api/member/Index')
            .then(response => response.json() as Promise<HeistMemberData[]>)
            .then(data => {
                this.setState({ heistMemberList: data, loading: false });
            });

        // This binding is necessary to make "this" work in the callback
        this.handleDelete = this.handleDelete.bind(this);
        this.handleEdit = this.handleEdit.bind(this);

    }

    public render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : this.renderHeistMemberTable(this.state.heistMemberList);

        return <div>
            <h1>Heist member Data</h1>
            <p>
                <Link to="/addHeistMember">Create New Heist member</Link>
            </p>
            {contents}
        </div>;
    }

    // Delete request for member
    private handleDelete(id: number) {
        if (!confirm("Do you want to delete heist member with Id: " + id))
            return;
        else {
            fetch('api/member/deleteHeistMember/' + id, {
                method: 'deleteHeistMember'
            }).then(data => {
                this.setState(
                    {
                        heistMemberList: this.state.heistMemberList.filter((rec) => {
                            return (rec.heistMemberId != id);
                        })
                    });
            });
        }
    }

    private handleEdit(id: number) {
        this.props.history.push("/member/EditHeistMember/" + id);
    }

    // Render() method returns the html table
    private renderHeistMemberTable(heistMemberList: HeistMemberData[]) {
        return <table className='table'>
            <thead>
                <tr>
                    <th></th>
                    <th>HeistMemberId</th>
                    <th>Name</th>
                    <th>Sex</th>
                    <th>Email</th>
                    <th>Skills</th>
                    <th>MainSkill</th>
                    <th>Status</th>
                </tr>
            </thead>
            <tbody>
                {heistMemberList.map(emp =>
                    <tr key={emp.heistMemberId}>
                        <td></td>
                        <td>{emp.heistMemberId}</td>
                        <td>{emp.name}</td>
                        <td>{emp.sex}</td>
                        <td>{emp.email}</td>
                        <td>{emp.skills}</td>
                        <td>
                            <a className="action" onClick={(id) => this.handleEdit(emp.heistMemberId)}>Edit</a>  |
                            <a className="action" onClick={(id) => this.handleDelete(emp.heistMemberId)}>Delete</a>
                        </td>
                        <td>{emp.mainSkill}</td>
                        <td>{emp.status}</td>
                    </tr>
                )}
            </tbody>
        </table>;
    }
}


export class HeistMemberData {
    heistMemberId: number = 0;
    name: string = "";
    sex: string = "";
    email: string = "";
    skills: string = "";
    mainSkill: string = "";
    status: string = "";
}