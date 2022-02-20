import Credit from "../views/credit"
import Refinance from "../views/refinance"
import Lease from "../views/lease"
import { createRouter, createWebHistory } from "vue-router"

const routes = [
    {
        path: "/credit",
        name: "credit-view",
        component: Credit
    },
    {
        path: "/refinance",
        name: "refinance-view",
        component: Refinance
    },
    {
        path: "/lease",
        name: "lease-view",
        component: Lease
    }
]

const router = createRouter({
    history: createWebHistory(process.env.BASE_URL),
    routes,
})

export default router