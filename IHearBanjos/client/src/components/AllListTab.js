import React from "react";
import { Link } from "react-router-dom";
import { Card, CardBody, Button } from "reactstrap";
import { addFavorite } from "../modules/tabManager";
import "./Tab.css"

export const AllListTab = ({ tab }) => {

    const banjoistFavorite = {
        tabId: tab.id,
        banjoistId: 0,
    };

    return (
        <Card className='tabCard'>
            <p className="text-left px-2">Posted by: {tab.banjoist.name}</p>
            <CardBody>
                <p>
                    <strong>Tab: <Link to={`/tab/${tab.id}`}>{tab.title.toUpperCase()}</Link></strong>
                </p>
                <p>Type: {tab.type.name}</p>
                <p>Difficulty: {tab.difficulty.name}</p>
                <Button onClick={() => addFavorite(banjoistFavorite)}>Add to Favorites</Button>
            </CardBody>
        </Card>
    )
}