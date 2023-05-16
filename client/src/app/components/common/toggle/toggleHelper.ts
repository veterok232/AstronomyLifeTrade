export type ToggleStateModel = [enabled: boolean, toggle: (val: boolean) => void];

export const switchToggle = (toggleState: ToggleStateModel) => {
    toggleState[1](!toggleState[0]);
};