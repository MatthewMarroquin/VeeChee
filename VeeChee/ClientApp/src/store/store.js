import { createStore } from "vuex" 

const store = createStore({
   state:{
    isUserLoggedIn: false,
      user:  null,
      token: null
   },
   mutations: {
    setToken(state, token) {
        state.token = token;
        if (token) {
            state.isUserLoggedIn = true;
        } else {
            state.isUserLoggedIn = false;
        }
    },
    setUser(state, user) {
        state.user = user; 
    },
    setUserLogged(state) {
      state.isUserLoggedIn = true
     },
     setUserLoggedOut(state) {
        state.isUserLoggedIn = false;
        state.token = null;
        state.user = null;
       }
},
actions: {
    setToken ({commit}, token) {
        commit("setToken", token);
    },
    setUser ({commit}, user) {
        commit("setUser", user);
    },
    setUserLogged ({commit}) {
      commit("setUserLogged", true);
     },
     setUserLoggedOut ({commit}) {
        commit("setUserLoggedOut", true);
       }
}
})




export default store
