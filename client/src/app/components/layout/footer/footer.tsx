import { useState } from "react";
import useAsyncEffect from "use-async-effect";
import { datesStore } from "../../../infrastructure/stores/datesStore";
import React from "react";
import { localizer } from "../../localization/localizer";
import { applicationConfig } from "../../../applicationConfig";

function Footer() {
    const [currentYear, setCurrentYear] = useState<number>();

    useAsyncEffect(async () => {
        setCurrentYear(await datesStore.getCurrentYear());
    }, []);

    return <footer className="grid__footer footer">
        {localizer.get("Footer", {
            "year": currentYear,
            "version": applicationConfig.clientAppVersion,
        })}
    </footer>;
}

export default Footer;