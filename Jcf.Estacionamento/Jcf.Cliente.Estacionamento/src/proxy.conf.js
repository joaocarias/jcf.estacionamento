const PROXY_CONFIG = [
  {
    "/api": {
      target: "https://localhost:7020",
      "secure": false
    },
    "pathRewrite": {
      "^/api": ""
    },
    "changeOrigin": true,
    "logLevel": "debug"
  }
]

module.exports = PROXY_CONFIG;
