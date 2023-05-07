import ReactDOM from "react-dom/client";

export function renderRootElement(root: JSX.Element) {
    ReactDOM.createRoot(document.getElementById("root")).render(root);
}