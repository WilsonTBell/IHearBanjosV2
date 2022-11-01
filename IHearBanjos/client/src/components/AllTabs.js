import React, { useEffect, useState } from "react";
import { getAllTabs } from "../modules/tabManager";
import { AllListTab } from "./AllListTab";
import { useNavigate } from "react-router-dom";
import { Button } from "reactstrap";

export const AllTabs = () => {
    const [tabs, setTabs] = useState([])
    const navigate = useNavigate()

    const getTabs = () => {
        getAllTabs().then(tabs => setTabs(tabs))
    }

    useEffect(() => {
        getTabs();
    }, []);

    return (
        <div>
            <Button
                onClick={() => navigate("add")}
                className="btn btn-primary">
                New Tab
            </Button>
            <div className="container">
                <div className="row justify-content-center">
                    {tabs?.map((tab) => (
                        <AllListTab tab={tab} key={tab?.id} user={tab.banjoist} />
                    ))}
                </div>
            </div>
        </div>
    )
}