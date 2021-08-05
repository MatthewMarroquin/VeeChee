<template>
  <div class="loginbox">
    <p></p>
    <h1>Sign in</h1>
    <div class="login-error" v-if="error">Error {{ errormsg }}</div>
    <label for="emailAddr">Email address</label>
    <input id="emailAddr" class="form-control form-control-lg center-block" />

    <label for="pw">Password</label>
    <input
      id="pw"
      type="password"
      class="form-control form-control-lg center-block"
      v-on:keyup.enter="login"
    />
    <p></p>
    <button    
      type="button"
      @click="login"
      class="medium-btn center-block"
      style="height: 55px; width: 230px"
    >
      <div v-if="loading" class="spinner-border spinner-border-sm"></div>
      Sign In
    </button>

    <p class="forgot-password">
      <router-link to="/forgot-password">Forgot password ?</router-link>
    </p>
    <p class="forgot-password">
      <router-link to="/Signup">Sign up or Create an Account</router-link>
    </p>
  </div>
</template>

<script>
import axios from "axios";
axios.defaults.withCredentials = true
export default {
  name: "Login",
  data() {
    return {
      loading: false,
      error: false,
      errormsg: "",
    };
  },
  methods: {
    async login() {
      if (this.loading) {
        return;
      }
      //reset the components
      this.loading = false;
      this.error = false;
      this.errormsg = "";
      //begin the login process
      this.loading = true;
      try {
        const emailAddress = document.getElementById("emailAddr").value;
        const pw = document.getElementById("pw").value;
        if (emailAddress.length && pw.length) {
          const request = JSON.stringify({
            requestType: "login",
            email: emailAddress,
            password: pw,
          });

          const response = await axios.post("/api/User/Login", request, {
            headers: { "Content-Type": "application/json" },
          });
          this.loading = false;
          if (response.status == 200) {
            if (response.data["Message"] == "Success") {
              this.$store.dispatch("setToken", response.data["UserId"]);
              this.$store.dispatch("setUser", response.data["RoleId"]);
              this.$store.dispatch("setUserLogged");

              this.$router.replace({ name: "LoginHome" });
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
    },
  },
};
</script>