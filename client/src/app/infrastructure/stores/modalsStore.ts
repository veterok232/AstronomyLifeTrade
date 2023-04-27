import { makeAutoObservable } from "mobx";
import { sharedHistory } from "../sharedHistory";

interface ModalData {
    modalType: string;
    modalProps?: any;
    className?: string;
    size?: string;
    disallowCloseOnClickOutside?: boolean;
}

export interface OpenedModalData extends ModalData {
    isVisible: boolean;
    creationOrderInSameModalTypeGroup: number;
}

class ModalsStore {
    public openedModals: OpenedModalData[] = [];

    constructor() {
        makeAutoObservable(this);
        sharedHistory.onPop(() => this.closeAllModals());
    }

    public openModal(modalData: ModalData) {
        this.openedModals.push({
            ...modalData,
            isVisible: true,
            creationOrderInSameModalTypeGroup: this.openedModals.filter(m => m.modalType === modalData.modalType).length,
        });
    }

    // close only first opened modal of given type
    public closeModal(modalType: string) {
        const modal = this.openedModals.find(m => m.modalType === modalType);

        if (modal) {
            // set isVisible to false so the window closes smoothly
            modal.isVisible = false;
        }
    }

    // find and remove only first opened modal of given type
    public removeModalFromStore(modalType: string) {
        const modal = this.openedModals.find(m => m.modalType === modalType);
        if (modal) {
            this.openedModals = this.openedModals.filter(m =>
                m.modalType !== modalType ||
                m.creationOrderInSameModalTypeGroup !== modal.creationOrderInSameModalTypeGroup);
        }
    }

    public closeAllModals() {
        this.openedModals.forEach(m => m.isVisible = false);
    }
}

export const modalsStore = new ModalsStore();