/// <binding BeforeBuild='clean, run:npm_start' />
module.exports = function (grunt) {
    grunt.initConfig({
        clean: ["wwwroot/js/*"],
        run: {
            npm_start: {
                exec: 'npm run start'
            }
        }
    });

    grunt.loadNpmTasks("grunt-contrib-clean");
    grunt.loadNpmTasks('grunt-run');
};