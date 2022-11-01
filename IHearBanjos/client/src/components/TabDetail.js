import { useEffect, useState } from "react"
import { useParams } from "react-router-dom"
import { Card, CardBody } from "reactstrap"
import { getTabById } from "../modules/tabManager"
import { IKImage } from 'imagekitio-react';

export const TabDetail = () => {
    const [tab, setTab] = useState(null)
    const { tabId } = useParams()

    const urlEndpoint = 'https://ik.imagekit.io/thv8ujxgn/';

    const getTab = (id) => {
        getTabById(id).then(data => {
            setTab(data)
        })
    }

    useEffect(() => {
        getTab(tabId);
    }, []);

    if (!tab) { return null }
    return (
        <div className="container">
            <div className="row justify-content-center">
                <IKImage
                    urlEndpoint={urlEndpoint}
                    path={tab.imageLocation}
                />
                <Card>
                    <CardBody>
                        <p>
                            <strong className="row justify-content-center">{tab.title.toUpperCase()}</strong>
                        </p>
                        <p className="row justify-content-center">{tab.type.name}</p>
                        <p className="row justify-content-center">{tab.difficulty.name}</p>
                        <p className="row justify-content-space-between">
                            <div>{tab.banjoist.name}</div>
                        </p>
                        <p>{tab.description}</p>
                    </CardBody>
                </Card>
            </div>
        </div>
    )
}