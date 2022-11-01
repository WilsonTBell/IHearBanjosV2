import React from "react";
import { useNavigate, Link } from "react-router-dom";
import { Card, CardBody, Button } from "reactstrap";
import { deleteTab } from "../modules/tabManager";

export const MyListTab = ({ tab }) => {
    const navigate = useNavigate()

    return (
        <Card>
            <p className="text-left px-2">Posted by: {tab.banjoist.name}</p>
            <CardBody>
                <p>
                    <strong>Tab: <Link to={`/tab/${tab.id}`}>{tab.title.toUpperCase()}</Link></strong>
                </p>
                <p>Type: {tab.type.name}</p>
                <p>Difficulty: {tab.difficulty.name}</p>
                <Button onClick={() => navigate(`/tab/edit/${tab.id}`)}>Edit</Button>
                <Button onClick={() => { deleteTab(tab.id); window.location.reload(false) }}>Delete</Button>
            </CardBody>
        </Card >
    )
}