module.exports = {
    rules: {
        "array-bracket-spacing": ["warn", "never"],
        camelcase: ["warn", { properties: "always" }],
        "comma-spacing": ["warn", { before: false, after: true }],
        "comma-style": [
            "warn",
            "last",
            {
                exceptions: {
                    ArrayExpression: false,
                    ArrayPattern: false,
                    ArrowFunctionExpression: false,
                    CallExpression: false,
                    FunctionDeclaration: false,
                    FunctionExpression: false,
                    ImportDeclaration: false,
                    ObjectExpression: false,
                    ObjectPattern: false,
                    VariableDeclaration: false,
                    NewExpression: false
                }
            }
        ],
        "computed-property-spacing": ["warn", "never"],
        "func-call-spacing": ["warn", "never"],
        "key-spacing": ["warn", { beforeColon: false, afterColon: true }],
        "keyword-spacing": ["warn", { before: true, after: true }],
        "no-bitwise": "warn",
        "no-continue": "warn",
        "no-lonely-if": "warn",
        "no-mixed-spaces-and-tabs": "warn",
        "no-spaced-func": "warn",
        "no-tabs": "warn",
        "no-unneeded-ternary": ["warn", { defaultAssignment: false }],
        "no-whitespace-before-property": "warn",
        "nonblock-statement-body-position": ["warn", "beside"],
        "one-var": ["warn", "never"],
        "one-var-declaration-per-line": ["warn", "always"],
        "operator-assignment": ["warn", "always"],
        "padded-blocks": [
            "warn",
            {
                blocks: "never",
                classes: "never",
                switches: "never"
            },
            {
                allowSingleLineBlocks: true
            }
        ],
        "semi": ["warn", "always"],
        "semi-spacing": ["warn", { before: false, after: true }],
        "semi-style": ["warn", "last"],
        "space-before-function-paren": [
            "warn",
            {
                anonymous: "always",
                named: "never",
                asyncArrow: "always"
            }
        ],
        "space-in-parens": ["warn", "never"],
        "space-infix-ops": "warn",
        "space-unary-ops": [
            "warn",
            {
                words: true,
                nonwords: false
            }
        ],
        "switch-colon-spacing": ["warn", { after: true, before: false }],
        "template-tag-spacing": ["warn", "never"],
        "unicode-bom": ["warn", "never"],
        "quotes": ["warn", "double"],
        "@typescript-eslint/ban-types": [
            "warn",
            {
                "extendDefaults": true,
                "types": {
                    "{}": false
                }
            }
        ],
        "@typescript-eslint/no-floating-promises": "warn",
        "@typescript-eslint/no-unsafe-assignment": "off",
        "@typescript-eslint/no-unsafe-member-access": "off",
        "@typescript-eslint/restrict-template-expressions": "off",
        "@typescript-eslint/no-unsafe-return": "off",
        "@typescript-eslint/no-unsafe-call": "off",
        "@typescript-eslint/restrict-plus-operands": "off",
        "@typescript-eslint/no-empty-function": "off",
        "@typescript-eslint/no-empty-interface": "off",
        "@typescript-eslint/explicit-function-return-type": "off",
        "@typescript-eslint/explicit-module-boundary-types": "off",
        "@typescript-eslint/no-misused-promises": [
            "error",
            {
                checksVoidReturn: {
                    attributes: false,
                },
            }
        ],
    }
};
