import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { Button, Form, FormGroup, Input, Label } from "reactstrap";
import { IKContext, IKUpload } from 'imagekitio-react';
import { getTabById } from "../modules/tabManager";
import { getAllTypes } from "../modules/typeManager";
import { getAllDifficulties } from "../modules/difficultyManager";
import { editTab } from "../modules/tabManager";


export const TabEdit = ({ }) => {
    const navigate = useNavigate()
    const { tabId } = useParams()
    const [types, setTypes] = useState([])
    const [difficulties, setDifficulties] = useState([])
    const [tab, setTab] = useState({
        id: 0,
        title: "",
        description: "",
        imageLocation: "",
        typeId: 0,
        difficultyId: 0,
    });

    const getTab = () => {
        getTabById(tabId).then(tab => setTab(tab));
    }

    useEffect(
        () => {
            getTab()
        },
        []
    )

    const urlEndpoint = 'https://ik.imagekit.io/thv8ujxgn/';
    const publicKey = 'public_lFDSRsjYBbat1QV6agMJFDDqUo4=';
    const authenticationEndpoint = 'http://localhost:3001/auth';

    const getTypes = () => {
        getAllTypes().then(types => setTypes(types))
    }

    useEffect(() => {
        getTypes();
    }, []);

    const getDifficulties = () => {
        getAllDifficulties().then(difficulties => setDifficulties(difficulties))
    }

    useEffect(() => {
        getDifficulties();
    }, []);


    const onError = err => {
        console.log("Error", err);
    };

    const onSuccess = res => {
        console.log("Success", res);
        const copy = { ...tab }
        copy.imageLocation = res.filePath
        setTab(copy)
    };


    const handleSaveButtonClick = (event) => {
        event.preventDefault()

        return editTab(tab)
            .then(() => {
                navigate("/mytabs")
            })
    }

    return <>


        <Form className="tabForm">
            <FormGroup>
                <Label for="Title">Title</Label>
                <Input
                    id="tab"
                    name="tab"
                    type="text"
                    value={tab.title}
                    onChange={
                        (evt) => {
                            const copy = { ...tab }
                            copy.title = evt.target.value
                            setTab(copy)
                        }
                    }
                />
            </FormGroup>
            <FormGroup>
                <Label for="Description">Description</Label>
                <Input
                    id="tab"
                    name="tab"
                    type="text"
                    value={tab.description}
                    onChange={
                        (evt) => {
                            const copy = { ...tab }
                            copy.description = evt.target.value
                            setTab(copy)
                        }
                    }
                />
            </FormGroup>
            <Label for="ImageUpload">Upload your Tab Image File</Label>
            <IKContext
                urlEndpoint={urlEndpoint}
                publicKey={publicKey}
                authenticationEndpoint={authenticationEndpoint}
            >
                <IKUpload
                    onError={onError}
                    onSuccess={onSuccess}
                />
            </IKContext>
            <FormGroup>
                <div>
                    <Dropdown
                        label="Song Type"
                        options={types}
                        onChange={(evt) => {
                            const copy = { ...tab }
                            copy.typeId = parseInt(evt.target.value)
                            setTab(copy)
                        }}
                    />
                </div>
            </FormGroup>
            <FormGroup>
                <div>
                    <Dropdown
                        label="Song Difficulty"
                        options={difficulties}
                        onChange={(evt) => {
                            const copy = { ...tab }
                            copy.difficultyId = parseInt(evt.target.value)
                            setTab(copy)
                        }}
                    />
                </div>
            </FormGroup>
            <FormGroup>
                <Button onClick={(clickEvent) => handleSaveButtonClick(clickEvent)}
                    className="btn btn-primary">Save</Button>
            </FormGroup>
        </Form>
        <Button onClick={() => navigate("/tab")}>Cancel</Button>


    </>

}

const Dropdown = ({ label, options, onChange }) => {
    return (
        <div>
            <Label for={label} >{label}</Label>
            <select onChange={(evt) => onChange(evt)}>
                <option value={0}>Select your {label}</option>
                {options.map((option) => (
                    <option value={option.id}>{option.name}</option>
                ))}
            </select>
        </div>
    );
};
