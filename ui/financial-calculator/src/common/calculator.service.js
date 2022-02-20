import axios from "./config";

const CalculatorService = {
    calculate(payload) {
        return axios.post(`/Credit/CalculateCredit`, payload);
    }
}

export default CalculatorService;