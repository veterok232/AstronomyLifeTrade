const webpackMerge = require("webpack-merge");
const getCommonConfig = require("./webpack.common.js");
const globImporter = require("node-sass-glob-importer");
const ESLintPlugin = require('eslint-webpack-plugin');
const eslintOptions = require("./plugins/eslintOptions");

module.exports = function (env) {
    var commonConfig = getCommonConfig(env);
    return webpackMerge(commonConfig, {
        mode: "development",
        devtool: "inline-source-map",
        module: {
            rules: [
                {
                    test: /\.css$/i,
                    use: [
                        {
                            loader: "style-loader"
                        },
                        {
                            loader: "css-loader",
                            options: {
                                sourceMap: false,
                            }
                        },
                    ]
                },
                {
                    test: /\.s[ac]ss$/i,
                    use: [
                        {
                            loader: "style-loader"
                        },
                        {
                            loader: "css-loader",
                            options: {
                                sourceMap: false,
                            }
                        },
                        {
                            loader: "sass-loader",
                            options: {
                                sassOptions: {
                                    importer: globImporter(),
                                    sourceMap: false,
                                }
                            }
                        }
                    ]
                }
            ]
        },
        plugins: [
            new ESLintPlugin(eslintOptions.getDevOptions())
        ],
        output: {
            publicPath: "/"
        },
        devServer: {
            port: 3000,
            static: {
                directory: "./src",
                watch: {
                    ignored: ["**/node_modules/", "**/dist/**"],
                },
            },
            historyApiFallback: true,
            proxy: {
                "/api": {
                    target: "https://localhost:5001",
                    secure: false
                }
            }
        },
    });
}