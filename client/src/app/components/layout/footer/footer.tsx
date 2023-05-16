import React from "react";
import { localizer } from "../../localization/localizer";
import { applicationConfig } from "../../../applicationConfig";

function Footer() {
    return <footer className="grid__footer footer">
        {localizer.get("Footer", {
            "year": 2023,
            "version": applicationConfig.clientAppVersion,
        })}
    </footer>;
}

export default Footer;