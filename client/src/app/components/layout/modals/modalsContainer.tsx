import { observer } from "mobx-react-lite";
import React from "react";
import { modalsStore } from "../../../infrastructure/stores/modalsStore";
import { Modal } from "reactstrap";
import { modalsComponents } from "./modalsComponents";

export const ModalsContainer = observer(() => {
    return (<>
        {modalsStore.openedModals.map(m => {
            const onToggle = () => {
                if (!m.disallowCloseOnClickOutside) {
                    modalsStore.closeModal(m.modalType);

                    if (m.modalProps.onClose){
                        m.modalProps.onClose();
                    }
                }
            };

            const onModalClosingTransitionOver = () => {
                modalsStore.removeModalFromStore(m.modalType);
            };

            const Component = modalsComponents[m.modalType];

            return <Modal key={`${m.modalType}-${m.creationOrderInSameModalTypeGroup}`}
                className={m.className}
                isOpen={m.isVisible}
                size={m.size}
                toggle={onToggle}
                onClosed={onModalClosingTransitionOver}>
                <Component {...m.modalProps} />
            </Modal>;
        })}
    </>);
});