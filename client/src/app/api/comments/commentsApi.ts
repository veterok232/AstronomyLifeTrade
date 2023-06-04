import { CommentsModel } from "../../components/catalog/comments/commentsSection";
import { PublishCommentModel } from "../../dataModels/comments/publishCommentModel";
import { httpGet, httpPost } from "../core/requestApi";

const resourceName = "comments";

export async function getProductComments(productId: string): Promise<CommentsModel> {
    return httpGet({
        url: `${resourceName}/get/${productId}`,
    });
}

export async function publishComment(model: PublishCommentModel) {
    return httpPost({
        url: `${resourceName}/publish`,
        body: model,
    });
}