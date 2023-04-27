import { generateRandomString } from "../../utils/stringUtils";
import { EnumDictionary } from "../enums/enumDictionary";

export class CrossWindowEventBroker {
    private readonly windowStorageEvenName = "storage";

    private listeners: EnumDictionary<string, EnumDictionary<string, (data?: string) => void>> = {};

    constructor() {
        window.addEventListener(this.windowStorageEvenName, this.storageEventHandler);
    }

    public publish(eventName: string, data?: string) {
        localStorage.setItem(eventName, data || generateRandomString());
        localStorage.removeItem(eventName);
    }

    public subscribe(eventName: string, handler: (data?: string) => void): string {
        const handlerReference = generateRandomString();
        if (!this.listeners[eventName]) {
            this.listeners[eventName] = {};
        }
        this.listeners[eventName][handlerReference] = handler;

        return handlerReference;
    }

    public unsubscribe(handlerRef: string) {
        for (const eventName in this.listeners) {
            if (this.listeners[eventName][handlerRef]) {
                delete this.listeners[eventName][handlerRef];
            }
            if (!this.listeners[eventName]) {
                delete this.listeners[eventName];
            }
        }
    }

    private storageEventHandler = (event: StorageEvent) => {
        if (!event.newValue || !this.listeners[event.key]) {
            return;
        }

        for (const handlerRef in this.listeners[event.key]) {
            this.listeners[event.key][handlerRef](event.newValue);
        }
    }
}

export const crossWindowEventBroker = new CrossWindowEventBroker();