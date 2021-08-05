<template>
    <h1 id="tableLabel">Weather forecast</h1>

    <p>This component demonstrates fetching data from the server.</p>

    <p v-if="!forecasts"><em>Loading...</em></p>

    <table class='table table-striped' aria-labelledby="tableLabel" v-if="forecasts">
        <thead>
            <tr>
                <th>Name</th>
            </tr>
        </thead>
        <tbody>
            <tr v-for="forecast of forecasts" v-bind:key="forecast">
                <td>{{ forecast }}</td>
            </tr>
        </tbody>
    </table>
</template>


<script>
    import axios from 'axios'
    // import global from '..composables/global.js'
    // import {toRefs, getCurrentInstance} from 'vue';
    export default {
        name: "FetchData",
        // setup(){
        //         const internalState = getCurrentInstance();
        //         const{state} = global;
        //         console.log(internalState);
        //         return {...toRefs(state)}
        // },
        data() {
            return {
                forecasts: []
            }
        },
        methods: {
            getWeatherForecasts() {
                
                axios.get('/weatherforecast')
                    .then((response) => {
                        this.forecasts =  response.data;
                    })
                    .catch(function (error) {
                        alert(error);
                    });
            }
        },
        mounted() {
            this.getWeatherForecasts();
            //console.log(this.$store.state.user.username);
        }
    }
</script>