import { createWebHistory, createRouter } from "vue-router";
import Home from "@/components/Home.vue";
import Counter from "@/components/Counter.vue";
import FetchData from "@/components/FetchData.vue";
import Login from "@/components/Login.vue";
import Signup from "@/components/Signup.vue";
import LoginHome from "@/components/LoginHome.vue";

const routes = [
    {
        path: "/",
        name: "Home",
        component: Home,
    },
    {
        path: "/Counter",
        name: "Counter",
        component: Counter,
    },
    {
        path: "/FetchData",
        name: "FetchData",
        component: FetchData,
    },
    {
        path: "/Login",
        name: "Login",
        component: Login,
    },
    {
        path: "/Signup",
        name: "Signup",
        component: Signup,
    },
    {
        path: "/LoginHome",
        name: "LoginHome",
        component: LoginHome,
    }
];

const router = createRouter({
    history: createWebHistory(),
    routes,
});

export default router;