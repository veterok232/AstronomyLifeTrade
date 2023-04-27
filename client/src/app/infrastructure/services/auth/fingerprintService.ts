import FingerprintJS, { Agent, UnknownComponents } from "@fingerprintjs/fingerprintjs";

let agent: Agent;

export async function initializeFingerprintAgent() {
    agent = await FingerprintJS.load();
}

function filterFingerprintComponents(components: UnknownComponents): UnknownComponents {
    delete components.colorDepth;
    delete components.screenResolution;
    delete components.availableScreenResolution;
    delete components.plugins;
    delete components.touchSupport;
    delete components.languages;

    return components;
}

export async function getFingerprint(): Promise<string> {
    const fingerprintComponents = (await agent.get()).components;
    return FingerprintJS.hashComponents(filterFingerprintComponents(fingerprintComponents));
}