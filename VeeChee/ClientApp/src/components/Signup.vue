<template>
    <div class="loginbox">
        <p></p>
        <h1>Sign up</h1>
        <div class="login-error" v-if="error">
            Error {{errormsg}}
        </div>

        <div class="negative-margin"></div>
        <div class="negative-margin"></div>
        <label for="userName">Username</label>
        <input id="userName" class="form-control form-control-lg center-block" />

        <div class="negative-margin"></div>
        <label for="emailAddr">Email address</label>
        <input id="emailAddr" class="form-control form-control-lg center-block" />

        <div class="negative-margin"></div>
        <label for="pw">Password</label>
        <input id="pw" type="password" v-on:keyup.enter="signup" class="form-control form-control-lg center-block" />
        <p></p>
        <button type="button" @click="signup" class="medium-btn center-block" style="height: 55px; width: 230px">
            <div v-if="loading" class="spinner-border spinner-border-sm"></div>
            Sign Up
        </button>

        <p class="forgot-password">
            <router-link to="/Login">Already have an Account</router-link>
        </p>

    </div>
</template>

<script>
    import axios from 'axios'
    export default {
        name: 'Signup',
        data() {
            return {
                loading: false,
                success: false,
                error: false,
                errormsg: ""
            }
        },
        methods: {
            async signup() {
                if (this.loading) {
                    return;
                }
                //reset the components
                this.loading = false;
                this.success = false;
                this.error = false;
                this.errormsg = "";
                //begin the login process
                this.loading = true;
                try {
                    const emailAddress = document.getElementById('emailAddr').value;
                    const pw = document.getElementById('pw').value;
                    const user = document.getElementById('userName').value;
                    if (emailAddress.length && pw.length && user.length) {
                        const request = JSON.stringify({
                            requestType: "signup",
                            email: emailAddress,
                            password: pw,
                            username: user
                        });

                        const response = await axios.post('/api/User/Signup', request, { headers: { 'Content-Type': 'application/json' } });
                        this.loading = false;
                        if (response.status == 200) {
                            if (response.data["Message"] == "Success") {
                                this.success = true;
                            }
                            else{
                            this.error = true;
                            this.errormsg = response.data["Message"];
                            }
                            
                        }
                        else {
                            throw "An Unexpected Error has Occurred";
                        }

                    }

                    else if (!emailAddress.length) {
                        this.error = true;
                        this.errormsg = "Invalid Email";
                    }
                    else if (!pw.length) {
                        this.error = true;
                        this.errormsg = "Invalid Password";
                    }
                    else {
                        this.error = true;
                        this.errormsg = "Invalid Entries";
                    }
                }
                catch (error) {
                    this.loading = false;
                    this.error = true;
                    this.errormsg = "An Unexpected Error has Occurred";
                }
                this.loading = false;
            }
        }
    }
</script>