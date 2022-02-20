import Home from "../views/home";
import Credits from "../views/credits"
import { createRouter, createWebHistory } from "vue-router"

const routes = [
    {
        path: "/",
        name: "home-view",
        component: Home
    },
    {
        path: "/credits",
        name: "credits-view",
        component: Credits
    }
]

const router = createRouter({
    history: createWebHistory(process.env.BASE_URL),
    routes,
})

export default router