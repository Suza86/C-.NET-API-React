import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { Link, NavLink } from 'react-router-dom';
import { HeistMemberData } from './FetchHeistMember';

interface AddHeistMemberData {
    title: string;
    loading: boolean;
    skillsList: Array<any>;
    heistData: HeistMemberData;
}

export class AddHeistMember extends React.Component<RouteComponentProps<{}>, AddHeistMemberData> {
    constructor(props) {
        super(props);

        this.state = { title: "", loading: true, heistMemberList: [], heistData: new HeistMemberData };

        fetch('api/member/GetHeistMemberList')
            .then(response => response.json() as Promise<Array<any>>)
            .then(data => {
                this.setState({ skillsList: data });
            });

        var hmid = this.props.match.params["hmid"];

        // Edit heist member
        if (hmid > 0) {
            fetch('api/member/DetailsHeistMember/' + hmid)
                .then(response => response.json() as Promise<HeistMemberData>)
                .then(data => {
                    this.setState({ title: "EditHeistMember", loading: false, heistData: data });
                });
        }

        // Add heist member
        else {
            this.state = { title: "CreateHeistMember", loading: false, skillsList: [], heistData: new HeistMemberData };
        }

        // This binding is necessary to make "this" work in the callback  
        this.handleSave = this.handleSave.bind(this);
        this.handleCancel = this.handleCancel.bind(this);
    }

    public render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : this.renderCreateForm(this.state.skillsList);

        return <div>
            <h1>{this.state.title}</h1>
            <h3>Heist Member</h3>
            <hr />
            {contents}
        </div>;
    }

    // This will handle the submit form event 
    private handleSave(event) {
        event.preventDefault();
        const data = new FormData(event.target);

        // PUT request for Edit Heist member 
        if (this.state.heistData.heistMemberId) {
            fetch('api/member/EditHeistMember', {
                method: 'PUT',
                body: data,

            }).then((response) => response.json())
                .then((responseJson) => {
                    this.props.history.push("/fetchheistmember");
                })
        }

        // POST request for Add Heist member
        else {
            fetch('api/member/CreateHeistMember', {
                method: 'POST',
                body: data,

            }).then((response) => response.json())
                .then((responseJson) => {
                    this.props.history.push("/fetchheistmember");
                })
        }
    }

    // This will handle Cancel button click event
    private handleCancel(e) {
        e.preventDefault();
        this.props.history.push("/fetchheistmember");
    }

    // Method eturns the HTML Form 
    private renderCreateForm(skillList: Array<any>) {
        return (
            <form onSubmit={this.handleSave} >
                <div className="form-group row" >
                    <input type="hidden" name="heistMemberId" value={this.state.heistData.heistMemberId} />
                </div>
                < div className="form-group row" >
                    <label className=" control-label col-md-12" htmlFor="Name">Name</label>
                    <div className="col-md-4">
                        <input className="form-control" type="text" name="name" defaultValue={this.state.heistData.name} required />
                    </div>
                </div >
                <div className="form-group row">
                    <label className="control-label col-md-12" htmlFor="Sex">Sex</label>
                    <div className="col-md-4">
                        <select className="form-control" data-val="true" name="sex" defaultValue={this.state.heistData.sex} required>
                            <option value="">-- Select Sex --</option>
                            <option value="M">M</option>
                            <option value="F">F</option>
                        </select>
                    </div>
                </div >
                <div className="form-group row">
                    <label className="control-label col-md-12" htmlFor="Email" >Email</label>
                    <div className="col-md-4">
                        <input className="form-control" type="text" name="Email" defaultValue={this.state.heistData.emial} required />
                    </div>
                </div>
                <div className="form-group row">
                    <label className="control-label col-md-12" htmlFor="Skills">Skills</label>
                    <div className="col-md-4">
                        <select className="form-control" data-val="true" name="Skills" defaultValue={this.state.heistData.skills} required>
                            <option value="">-- Select Skills --</option>
                            {skillsList.map(skills =>
                                <option key={skills.heistSkillsId} value={skills.name}>{skills.level}</option>
                            )}
                        </select>
                    </div>
                </div >
                <div className="form-group">
                    <button type="submit" className="btn btn-default">Save</button>
                    <button className="btn" onClick={this.handleCancel}>Cancel</button>
                </div >
            </form >
        )
    }
}