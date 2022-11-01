import React, { useEffect, useState } from "react";
import { getFavoriteTabs } from "../modules/tabManager";
import { FavoriteListTab } from "./FavoriteListTab";

export const FavoriteTabs = () => {
    const [tabs, setTabs] = useState();

    useEffect(() => {
        getFavoriteTabs().then((tabs) => {
            setTabs(tabs);
        })
    }, []);

    return (
        <div className="container">
            <div className="row justify-content-center">
                {tabs?.map((tab) => (
                    <FavoriteListTab tab={tab} key={tab?.id} user={tab.banjoist} />
                ))}
            </div>
        </div>
    )
}