import { Pageable } from "../../../dataModels/common/pageable";
import { Sortable } from "../../../dataModels/common/sortable";
import { ExtPagingHandler } from "../controls/pagination/extPagingHandler";

export type FilterData<TFilterData> = TFilterData & Sortable & Pageable;
export type DataLoader<TFilterData> = (request: FilterData<TFilterData>) => Promise<void>;

export class ListRequestHandler<TFilterData> {
    private listOptions: Sortable & Pageable;
    private request: FilterData<TFilterData>;
    private dataLoader: DataLoader<TFilterData>;

    public pageHandler = new ExtPagingHandler();

    constructor(
        defaultFilter: TFilterData,
        defaultPaging: Pageable,
        defaultSorting: Sortable,
        dataLoader: DataLoader<TFilterData>
    ) {
        this.listOptions = { ...defaultPaging, ...defaultSorting };
        this.request = { ...defaultFilter, ...this.listOptions };
        this.dataLoader = dataLoader;
    }

    public getRequest = () => this.request;

    public applyFilter = (filter: TFilterData) => {
        if (this.listOptions.pageNumber > 1) {
            this.listOptions.pageNumber = 1;
            this.pageHandler.updatePage(1);
        }
        this.request = { ...filter, ...this.listOptions };

        return this.dataLoader(this.request);
    }

    public applyListOptions = async (val: Sortable | Pageable) => {
        this.listOptions = { ...this.listOptions, ...val };
        this.request = { ...this.request, ...val };
        await this.dataLoader(this.request);
    }
}