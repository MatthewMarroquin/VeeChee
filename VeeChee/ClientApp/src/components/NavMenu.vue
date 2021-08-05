<template>

    <header>
        <nav class="navbar navbar-expand-sm navbar-toggleable-sm navbar-light bg-white border-bottom box-shadow mb-3">
            <div class="container">                
                <a class="nav-item"> 
                    <router-link v-if="!auth" :to="{ name: 'Home' }" style="font-weight: bold;font-size: 2.5rem;font-family: Snell Roundhand, cursive;color: darkslateblue">VeeChee</router-link>
                    <router-link v-else :to="{ name: 'LoginHome' }" style="font-weight: bold;font-size: 2.5rem;font-family: Snell Roundhand, cursive;color: darkslateblue">VeeChee</router-link>
                </a>
                <div class="navbar-collapse collapse d-sm-inline-flex flex-sm-row-reverse"
                     v-bind:class="{show: isExpanded}">
                    <ul class="navbar-nav flex-grow">
                        <li class="nav-item">
                            <router-link v-if="!auth" :to="{ name: 'Home' }" class="nav-link text-dark">Home</router-link>
                            <router-link v-else :to="{ name: 'LoginHome' }" class="nav-link text-dark">Home</router-link>
                        </li>
                        <li class="nav-item">
                            <router-link :to="{ name: 'Counter' }" class="nav-link text-dark">Counter</router-link>
                        </li>
                        <li class="nav-item">
                            <router-link v-if="!auth" :to="{ name: 'Login' }" class="nav-link text-dark">Login</router-link>
                            <logout v-else @click="logout" class="nav-link text-dark">Logout</logout>
                        </li>
                        <li class="nav-item">
                            <router-link :to="{ name: 'FetchData' }" class="nav-link text-dark">Fetch Data</router-link>
                        </li>
                    </ul>
                </div>
            </div>
        </nav>
    </header>
</template>


<style>
    a.navbar-brand {
        white-space: normal;
        text-align: center;
        word-break: break-all;
    }

    html {
        font-size: 14px;
    }

    @media (min-width: 768px) {
        html {
            font-size: 16px;
        }
    }

    .box-shadow {
        box-shadow: 0 .25rem .75rem rgba(0, 0, 0, .05);
    }
</style>
<script>
    export default {
        name: "NavMenu",
        data() {
            return {
                isExpanded: false,
                isAuthenticated: this.$store.state.isUserLoggedIn
            }
        },
         computed: {
        auth(){
            return  this.$store.state.isUserLoggedIn
        }
    },
        methods: {
            collapse() {
                this.isExpanded = false;
            },

            toggle() {
                this.isExpanded = !this.isExpanded;
            },
            logout(){
                this.$store.dispatch("setUserLoggedOut");
                this.$router.replace({ name: "Home" });
            }
        }
    }
</script>