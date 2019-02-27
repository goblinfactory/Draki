var app = new Vue({

    el: '#app',
    data: {
        username: 'guest',
        password: '1234',
        loggedIn: false,
        showLogin: false,
        name: 'Fred Smith',
        message: 'Hello Vue!'
    },

    mounted: function () {
        console.log('loaded!');
        var imIn =  (localStorage.loggedIn == 'true');
        this.loggedIn = imIn;
    },

    methods: {

        logout: function () {
            localStorage.loggedIn = false;
            this.loggedIn = false;
            this.showLogin = false;
        },

        showLoginForm: function () {
            this.showLogin = true;
            console.log(this.showLogin);
        },

        cancelLogin: function () {
            this.showLogin = false;
        },

        dologin: function () {
            if (this.username == 'guest' && this.password == '1234') {
                this.loggedIn = true;
                this.showLogin = false;
                localStorage.loggedIn = true;
                console.log('logged in succesfully');
            }
            else {
                console.log('invalid username or password : ' + this.username + ',****');
            }
        }
    }

});