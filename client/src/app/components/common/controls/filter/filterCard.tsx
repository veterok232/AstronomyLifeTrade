import React from "react";
import { Card, Row, Collapse, Button } from "reactstrap";
import { Local } from "../../../localization/local";
import { AppIcon } from "../appIcon";
import { showNotificationIfInvalid } from "../validation/formValidators";
import { ToggleStateModel, switchToggle } from "../../toggle/toggleHelper";
import { scrollToTop } from "../../../../utils/windowUtils";
import { CollapseToggle } from "../collapse/collapseToggle";

interface Props {
    fullViewState: ToggleStateModel;
    primarySearchTitle?: string;
    primarySearchContent?: React.ReactNode;
    advancedSearchContent: React.ReactNode;
    handleSubmit: () => void;
    resetFilter: () => void;
    hasFormErrors: boolean;
}

export const FilterCard = (props: Props) => {
    const [fullView, setFullView] = props.fullViewState;

    const search = () => {
        if (props.hasFormErrors) {
            showNotificationIfInvalid(false);
            return;
        }

        props.handleSubmit();
        switchToggle([fullView, setFullView]);
        scrollToTop();
    };

    return (<Card className="ui-card p-3 my-3 filter-card">
        <div className="filter-header d-flex align-items-start flex-column flex-md-row">
            <div className="w-100 d-md-none text-center mb-1" onClick={() => switchToggle([fullView, setFullView])}>
                <AppIcon className="float-left" icon="filter_list" />
                <span><b><Local id={`${fullView ? "HideSearch" : "AdvancedSearch"}`} /></b></span>
                <hr className={!fullView ? "d-none d-md-block" : ""} />
            </div>
            {props.primarySearchTitle &&
                <h6 className={`mr-3 group-header ${!fullView ? "d-none d-md-block" : ""}`}>
                    <b><Local id={props.primarySearchTitle} />:</b>
                </h6>}
            {props.primarySearchContent  &&
                <Row className={`primary-search-content flex-grow-0 flex-md-grow-1 m-0 ${!fullView ? "d-none d-md-flex" : ""}`}>
                    {props.primarySearchContent}
                </Row>}
            <CollapseToggle openedLabelKey="HideSearch" closedLabelKey="AdvancedSearch" state={[fullView, setFullView]}
                className="text-right d-none d-md-block m-0 ml-auto search-toggle group-header" />
        </div>
        <Collapse isOpen={fullView}>
            <hr />
            <div className="px-lg-4">
                {fullView && props.advancedSearchContent}
                <Button color="primary float-right" onClick={search}><Local id="Search" /></Button>
                <Button className="bg-transparent text-primary float-right" onClick={props.resetFilter}><Local id="ClearAll" /></Button>
            </div>
        </Collapse>
    </Card>);
};