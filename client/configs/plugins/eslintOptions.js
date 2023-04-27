const path = require("path");

const context = path.resolve(__dirname, "../../src/app");
const extensions = ["ts", "tsx"];
const overrideConfigFile = path.resolve(__dirname, "../../.eslintrc.json");

module.exports = {
    getDevOptions,
    getProdOptions
};

function getProdOptions() {
    return {
        context: context,
        failOnWarning: true,
        extensions: extensions,
        overrideConfigFile: overrideConfigFile,
    }
}

function getDevOptions() {
    return {
        context: context,
        extensions: extensions,
        overrideConfigFile: overrideConfigFile,
    };
};