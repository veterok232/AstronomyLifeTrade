import { Comment } from "../catalog/comment";

export interface PublishCommentModel {
    comment: Comment;
    productId: string;
}