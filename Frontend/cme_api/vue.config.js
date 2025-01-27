const { defineConfig } = require('@vue/cli-service');
module.exports = defineConfig({
  transpileDependencies: true,
  devServer: {
    hot: true,
    watchFiles: {
      paths: ['src/**/*'],
      options: {
        usePolling: true,
      },
    },
  },
});