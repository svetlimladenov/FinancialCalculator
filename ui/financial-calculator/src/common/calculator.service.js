import axios from "./config";

const CalculatorService = {
    credit(payload) {
        return axios.post(`/Credit/CalculateCredit`, payload);
    },
    refinance(payload) {
        return axios.post(`/Credit/CalculateRefinanceCredit`, payload);
    },
    lease(payload) {
        return axios.post(`/Credit/CalculateLease`, payload);
    }
}

export default CalculatorService;