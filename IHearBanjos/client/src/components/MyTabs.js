import React, { useEffect, useState } from "react";
import { getMyTabs } from "../modules/tabManager";
import { MyListTab } from "./MyListTab";

export const MyTabs = () => {
    const [tabs, setTabs] = useState();

    useEffect(() => {
        getMyTabs().then((tabs) => {
            setTabs(tabs);
        })
    }, []);

    return (
        <div className="container">
            <div className="row justify-content-center">
                {tabs?.map((tab) => (
                    <MyListTab tab={tab} key={tab?.id} user={tab.banjoist} />
                ))}
            </div>
        </div>
    )
}