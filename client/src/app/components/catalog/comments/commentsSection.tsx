/* eslint-disable @typescript-eslint/no-unused-vars */
/* eslint-disable @typescript-eslint/require-await */
import { useState } from "react";
import { Form as FinalForm, FormRenderProps } from "react-final-form";
import { Comment } from "../../../dataModels/catalog/comment";
import useAsyncEffect from "use-async-effect";
import React from "react";
import { FieldArray } from "react-final-form-arrays";
import { Button, Col, Form, Row } from "reactstrap";
import { withParent } from "../../common/controls/formControls/formControlsDecorators";
import arrayMutators from "final-form-arrays";
import { CommentCard } from "./commentCard";
import { LabeledField } from "../../common/presentation/labeledField";
import { Rating } from "@mui/material";
import { NoData } from "../../common/presentation/noData";
import { TextAreaFormControl } from "../../common/controls/formControls/textareaFormControl";
import { Local } from "../../localization/local";
import { getProductComments, publishComment } from "../../../api/comments/commentsApi";
import { isConsumer } from "../../../infrastructure/services/auth/authService";
import { contextStore } from "../../../infrastructure/stores/contextStore";
import { FormApi } from "final-form";

export interface CommentsModel {
    comments: Comment[];
    commentsCount: number;
    averageRating: number;
}

interface CommentsFormData {
    commentsModel: CommentsModel;
    userComment: Comment;
}

interface Props {
    productId: string;
}

const testComments: CommentsModel = {
    comments: [{
        text: "Очень крутой телескоп. Всем доволен!",
        rating: 5,
        createdAt: new Date(),
        userName: "Ivan",
        userLastName: "Fedorovich",
    }, {
        text: "Спасибо! Очень качественный агрегат! Но в другом магазине на 50 копеек дешевле, поэтому 4 звезды",
        rating: 4,
        createdAt: new Date(),
        userName: "Ivan",
        userLastName: "Ivanov",
    }],
    commentsCount: 2,
    averageRating: 4.5,
};

export const CommentsSection = (props: Props) => {
    const [comments, setComments] = useState<CommentsModel>();
    const [rating, setRating] = useState<number>();

    useAsyncEffect(async () => {
        setComments(await getProductComments(props.productId));
    }, []);

    const onSubmit = async (
        formData: CommentsFormData,
        form: FormApi<CommentsFormData, Partial<CommentsFormData>>) => {
        await publishComment({
            comment: {
                ...formData.userComment,
                rating: rating,
            },
            productId: props.productId,
        });

        comments.comments.push({
            ...formData.userComment,
            createdAt: new Date(),
            userName: contextStore.firstName,
            userLastName: contextStore.lastName,
            rating: rating,
        });
        comments.commentsCount++;

        let sumRating = 0;

        for (let i = 0; i < comments.comments.length; i++) {
            sumRating += comments.comments[i].rating;
        }

        comments.averageRating = Math.round(sumRating / comments.comments.length);

        setComments(comments);
        setRating(0);
        form.reset();
    };

    return (
        <FinalForm
            onSubmit={onSubmit}
            initialValues={{
                commentsModel: comments,
            }}
            mutators={{...arrayMutators}}
            render={({ values, form, handleSubmit}: FormRenderProps<CommentsFormData>) => (
                <Form onSubmit={handleSubmit} className="comments-section">
                    <Row>
                        <Col>
                            <Row>
                                <Col className="col-1 pt-2">
                                    <div className="average-rating d-flex align-items-center justify-content-center">
                                        <span>{comments?.averageRating?.toFixed(1)}</span>
                                    </div>
                                </Col>
                                <Col className="col-11 pl-4">
                                    <Row>
                                        <Rating size="medium" name="commentsModel.averageRating" value={comments?.averageRating ?? 0} readOnly precision={0.1} />
                                    </Row>
                                    <Row>
                                        <LabeledField className="ml-2" labelKey={"AverageRating"} value={`Отзывов: ${comments?.commentsCount ?? 0}`} />
                                    </Row>
                                </Col>
                            </Row>
                        </Col>
                    </Row>
                    {isConsumer() &&
                        <Row>
                            <Col>
                                <Row className="mb-2">
                                    <Col className="pl-0">
                                        <h1 className="leave-comment-section pt-2"><Local id="LeaveYourComment"/></h1>
                                    </Col>
                                </Row>
                                <Row>
                                    <Col className="pl-0">
                                        <Rating name={"userComment.rating"} precision={1}
                                            onChange={(e, val) => setRating(val)} />
                                    </Col>
                                </Row>
                                <Row>
                                    <Col className="pl-0">
                                        <TextAreaFormControl name={"userComment.text"} placeholder="Оставьте свой отзыв об этом товаре" />
                                    </Col>
                                </Row>
                                <Row>
                                    <Col className="pl-0">
                                        <Button type="submit" className="float-right">
                                            <Local id="PublishComment"/>
                                        </Button>
                                    </Col>
                                </Row>
                            </Col>
                        </Row>
                    }
                    {comments?.commentsCount > 0
                        ? <FieldArray name="commentsModel.comments" className="m-0 w-100">
                                {({ fields }) => (
                                    <Row className="p-0">
                                        {fields.map((name, i) => (
                                            withParent(CommentCard, name, {
                                                comment: comments?.comments[i],
                                                ind: i,
                                                onRemoveItem: () => {
                                                    //await props.onRemoveItem(i);
                                                    fields.remove(i);
                                                },
                                            }
                                        )))}
                                    </Row>
                                )}
                            </FieldArray>
                        : <NoData localizationKey="NoComments"/>}

                </Form>
            )}
        />
    );
};