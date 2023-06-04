import React from "react";
import { Row, Col } from "reactstrap";
import { Comment } from "../../../dataModels/catalog/comment";
import { Rating } from "@mui/material";

interface Props {
    comment: Comment;
    ind: number;
    onRemoveItem: () => Promise<void>;
}

export const CommentCard = (props: Props) => {
    return (
        <Row className="comment-card w-100">
            <Col>
                <Row>
                    <Col>
                        <Rating size="small" name="rating" value={props.comment?.rating} readOnly precision={1} />
                    </Col>
                </Row>
                <Row>
                    <Col>
                        <span className="user-name">{props.comment?.userName}</span>
                    </Col>
                </Row>
                <Row>
                    <Col>
                        <span className="created-at">{props.comment?.createdAt.toString()}</span>
                    </Col>
                </Row>
                <Row className="mt-2">
                    <Col>
                        <span>{props.comment?.text}</span>
                    </Col>
                </Row>
                <hr />
            </Col>
        </Row>
    );
};