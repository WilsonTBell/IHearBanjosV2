import React, { useState, useEffect } from "react";
import { getBanjoists } from "../modules/banjoistManager";
import { Banjoist } from "./Banjoist";

export const Banjoists = () => {
    const [banjoists, setBanjoists] = useState();


    useEffect(() => {
        getBanjoists().then((banjoists) => {
            setBanjoists(banjoists)
        });
    }, []);

    return <>
        {
           banjoists?.map((up) => (
                <Banjoist banjoist={up} key={up.id} />
            ))}

    </>
}