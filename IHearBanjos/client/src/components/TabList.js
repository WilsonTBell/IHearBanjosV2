import { Tab } from "./AllListTab";
import { useNavigate } from "react-router-dom";
import { Button } from "reactstrap";

export const TabList = ({ tabs }) => {
    const navigate = useNavigate()

    return (
        <div className="container">
            <div className="row justify-content-center">
                {tabs?.map((tab) => (
                    <Tab tab={tab} key={tab?.id} user={tab.banjoist} />
                ))}
            </div>
        </div>
    )
}