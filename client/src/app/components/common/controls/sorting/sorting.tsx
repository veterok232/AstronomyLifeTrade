/* eslint-disable react-hooks/exhaustive-deps */
import React, { useState, useEffect } from "react";
import { useLocation } from "react-router-dom";
import { Dropdown, DropdownToggle, DropdownMenu, DropdownItem } from "reactstrap";
import { Sortable } from "../../../../dataModels/common/sortable";
import { SortOrder } from "../../../../dataModels/enums/sortOrder";
import { sortingDataStore } from "../../../../infrastructure/stores/sortingDataStore";
import { Local } from "../../../localization/local";
import { localizer } from "../../../localization/localizer";
import { AppIcon } from "../appIcon";

interface SortingProps {
    sortKeys: Array<string>;
    default?: Sortable;
    localizationPrefix?: string;
    onChange: (sortOptions: Sortable) => void;
}

const getLocalizedKey = (key: string, prefix?: string) => {
    const fullKey = prefix ? `${prefix}_${key}` : key;
    return localizer.get(fullKey);
};

function Sorting(props: SortingProps) {
    const [dropdownOpen, setDropdownOpen] = useState(false);
    const toggle = () => setDropdownOpen(prevState => !prevState);

    const [selectedKey, setSelectedKey] = useState(props.default?.sortBy);
    const [selectedOrder, setSelectedOrder] = useState(props.default?.direction || SortOrder.Ascending);
    const location = useLocation();

    useEffect(() => {
        const sortingData = sortingDataStore.getFilterData<Sortable>(location.key);

        if (sortingData) {
            setSelectedKey(sortingData.sortBy);
            setSelectedOrder(sortingData.direction);
            props.onChange(sortingData);
        }
    }, []);

    const handleChanges = (options: Sortable) => {
        if (options.sortBy !== selectedKey) {
            setSelectedKey(options.sortBy);
        }
        if (options.direction !== selectedOrder) {
            setSelectedOrder(options.direction);
        }

        sortingDataStore.saveFilterData<Sortable>(options, location.key);
        props.onChange(options);
    };

    return (
        <section className="sorting">
            <span className="sorting__sort-by-wrapper"
                onClick={() => selectedKey && handleChanges({
                    sortBy: selectedKey,
                    direction: selectedOrder === SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending
                })
                }>
                <span><Local id="SortBy" />:</span>
                {selectedKey && <span className="sorting__sort-by text-nowrap">
                    <span className="text-wrap">{getLocalizedKey(selectedKey, props.localizationPrefix)}</span>
                    <AppIcon className="sorting__order-icon"
                        icon={selectedOrder === SortOrder.Descending ? "arrow_downward" : "arrow_upward"} />
                </span>}
            </span>
            <Dropdown className="sorting__dropdown" isOpen={dropdownOpen} toggle={toggle}>
                <DropdownToggle className="sorting__toggle" color="">
                    <AppIcon className="sorting__arrow" icon="keyboard_arrow_down" />
                </DropdownToggle>
                <DropdownMenu className="sorting__menu" right={true}>
                    {props.sortKeys.map((key: string, i: number) =>
                        <DropdownItem key={i} className="sorting__menu-item"
                            onClick={() => handleChanges({ sortBy: key, direction: selectedOrder })}>
                            {selectedKey === key && <AppIcon className="sorting__checked-icon" icon="check" />}
                            {getLocalizedKey(key, props.localizationPrefix)}
                        </DropdownItem>)}
                </DropdownMenu>
            </Dropdown>
        </section>
    );
}

export default Sorting;