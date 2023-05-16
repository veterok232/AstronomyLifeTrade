import React, { useState } from "react";
import { Pagination, PaginationItem, PaginationLink } from "reactstrap";
import { Constants } from "../../../constants";
import { ExtPagingHandler } from "./extPagingHandler";

interface PaginationControlProps {
    totalPages: number;
    onChange: (pageNum: number) => void;
    showEdgeSwitches?: boolean;
    className?: string;
    extPageHandler?: ExtPagingHandler;
}

const maxItems = Constants.paging.maxPagingControlItemsCount;

export const PaginationControl = (props: PaginationControlProps) => {
    const [currentPage, setCurrentPage] = useState(1);

    const handlePageChange = (newPageNum: number) => {
        setCurrentPage(newPageNum);
        props.onChange(newPageNum);
    };

    const changePageExternally = (pageNum: number) => {
        setCurrentPage(pageNum);
    };

    if (props.extPageHandler) {
        props.extPageHandler.subscribe(changePageExternally);
    }

    const total = props.totalPages;

    if (!total) {
        return null;
    }

    const maxFirstItemNumber = Math.max(total - maxItems + 1, 1);
    const isEllipsisNeeded = (total > maxItems) && currentPage < maxFirstItemNumber + 1;
    const firstItemNumber = currentPage > 1
        ? Math.min(currentPage - 1, maxFirstItemNumber)
        : 1;
    const linksCount = isEllipsisNeeded
        ? Constants.paging.itemsBeforeEllipsisCount
        : Math.min(total, maxItems);

    return (
        <Pagination className={props.className}>
            {props.showEdgeSwitches && <PaginationItem>
                <PaginationLink first onClick={() => {
                    if (currentPage !== 1) {
                        handlePageChange(1);
                    }
                }} />
            </PaginationItem>}
            {currentPage !== 1 && <PaginationItem>
                <PaginationLink previous onClick={() => {
                    if (currentPage > 1) {
                        handlePageChange(currentPage - 1);
                    }
                }} />
            </PaginationItem>}
            {Array(linksCount).fill(firstItemNumber).map((x, ind) => {
                const pageNum = ind + firstItemNumber;
                return (
                    <PaginationItem key={pageNum} active={pageNum === currentPage}>
                        <PaginationLink onClick={() => {
                            if (pageNum != currentPage) {
                                handlePageChange(pageNum);
                            }
                        }}>
                            {pageNum}
                        </PaginationLink>
                    </PaginationItem>
                );
            })}
            {isEllipsisNeeded && <>
                <PaginationItem>
                    <span className="page-ellipsis d-block">...</span>
                </PaginationItem>
                <PaginationItem>
                    <PaginationLink onClick={() => {
                        if (currentPage !== total) {
                            handlePageChange(total);
                        }
                    }}>
                        {total}
                    </PaginationLink>
                </PaginationItem>
            </>}
            {currentPage !== total && <PaginationItem>
                <PaginationLink next onClick={() => {
                    if (currentPage < total) {
                        handlePageChange(currentPage + 1);
                    }
                }} />
            </PaginationItem>}
            {props.showEdgeSwitches && <PaginationItem>
                <PaginationLink last onClick={() => {
                    if (currentPage !== total) {
                        handlePageChange(total);
                    }
                }} />
            </PaginationItem>}
        </Pagination>
    );
};