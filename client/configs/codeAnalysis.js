module.exports = {
    ignorePatterns: ["**/*.js"],
    extends: ["./rules/style"].map(require.resolve)
};
