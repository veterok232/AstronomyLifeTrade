const webpack = require("webpack");
const HtmlWebpackPlugin = require("html-webpack-plugin");
const CopyWebpackPlugin = require("copy-webpack-plugin");
const dotenv = require("dotenv");
const path = require("path");
const fs = require('fs');

module.exports = function (env) {
    const envConfig = getEnvironmentConfiguration(env);

    return {
        entry: "./src/index.tsx",
        resolve: {
            extensions: [".tsx", ".ts", ".js", ".json"],
            modules: [path.resolve(__dirname, "src"), "node_modules"],
        },
        module: {
            rules: [
                {
                    test: /\.tsx?$/,
                    use: "ts-loader",
                    exclude: [/\.(d)\.ts$/, /node_modules/],
                },
                {
                    test: /\.(jpg|png|gif)$/,
                    use: 'file-loader'
                },
                {
                    test: /\.(eot|woff2?|svg|ttf)([\?]?.*)$/,
                    use: 'file-loader',
                }
            ],
        },
        plugins: [
            new CopyWebpackPlugin({
                patterns: [{
                    from: "src/static",
                    to: "static",
                }],
            }),
            new HtmlWebpackPlugin({
                template: "src/index.html"
            }),
            new webpack.DefinePlugin(envConfig)
        ],
    }
}

const getEnvironmentConfiguration = function(env) {
    const currentPath = path.join(__dirname);
    const basePath = `${currentPath}/.env`;
    const envPath = `${basePath}.${env.ENVIRONMENT}`;
    const finalPath = fs.existsSync(envPath) ? envPath : basePath;
    const fileEnv = dotenv.config({ path: finalPath }).parsed;

    return Object.keys(fileEnv).reduce((prev, next) => {
        prev[`process.env.${next}`] = JSON.stringify(fileEnv[next]);
        return prev;
    }, {});
}