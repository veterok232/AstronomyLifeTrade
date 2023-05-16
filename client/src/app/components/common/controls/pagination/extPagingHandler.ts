export class ExtPagingHandler {
    private handlePageChanging: (pageNum: number) => void;

    public subscribe(pageChangingHandler: (pageNum: number) => void) {
        this.handlePageChanging = pageChangingHandler;
    }

    public updatePage(pageNum: number) {
        if (this.handlePageChanging) {
            this.handlePageChanging(pageNum);
        }
    }
}