<template>
  <v-main>
    <v-form v-model="valid">
      <v-container>
        <v-row>
            <v-col
                cols="12"
                md="3"
            >
                <v-text-field
                    v-model="amount"
                    label="Стойност *"
                ></v-text-field>
            </v-col>
            <v-col
                cols="12"
                md="3"
            >
                <v-text-field
                    v-model="period"
                    label="Лизингов срок *"
                ></v-text-field>
            </v-col>
            <v-col
                cols="12"
                md="3"
            >
                <v-text-field
                    v-model="interest"
                    label="Лихва (%) *"
                ></v-text-field>
            </v-col>
            <v-col
                cols="12"
                md="3"
            >
                <v-text-field
                    v-model="initialPaymentPercentage"
                    label="Първоначална вноска (%) *"
                ></v-text-field>
            </v-col>
            <v-col 
              cols="12"
              md="3"
            >
                <v-btn @click="submit">
                    submit
                </v-btn>
            </v-col>
        </v-row>
      </v-container>
    </v-form>

    <v-table v-show="lease">
      <template v-slot:default>
        <tbody>
          <tr>
            <td>Стойност за лизинг:</td>
            <td>{{ lease?.leaseAmount }}</td>
          </tr>
          <tr>
            <td>Първоначална вноска:</td>
            <td>{{ lease?.initialPayment }}</td>
          </tr>
          <tr>
              <td>Лизингов срок:</td>
              <td>{{ lease?.period }}</td>
          </tr>
          <tr>
            <td>Месечна вноска:</td>
            <td>{{ lease?.monthlyInstalment }}</td>
          </tr>
          <tr>
            <td>Остатъчна главница (чиста стойност на кредита):</td>
            <td>{{ lease?.remainingAmount }}</td>
          </tr>
          <tr>
              <td>Оскъпяване на актива за периода (сума):</td>
              <td>{{ lease?.totalIncrease }}</td>
          </tr>
          <tr>
              <td>Обща сума за изплащане:</td>
              <td>{{ lease?.totalPrice }}</td>
          </tr>
        </tbody>
      </template>
    </v-table>

    <v-table v-show="payments.length">
      <template v-slot:default>
        <thead>
          <tr>
            <th class="text-left">
              №
            </th>
            <th class="text-left">
              Дата
            </th>
            <th class="text-left">
              Месечна вноска
            </th>
            <th class="text-left">
              Вноска главница
            </th>
            <th class="text-left">
              Вноска лихва
            </th>
            <th class="text-left">
              Остатък главница
            </th>
          </tr>
        </thead>
        <tbody>
          <tr
            v-for="(item, index) in payments"
            :key="index"
          >
            <td>{{ index + 1 }}</td>
            <td>{{ item.date }}</td>
            <td>{{ item.monthlyPayment }}</td>
            <td>{{ item.monthlyPrincipal }}</td>
            <td>{{ item.monthlyInterest }}</td>
            <td>{{ item.leftPrincipal }}</td>
          </tr>
        </tbody>
      </template>
    </v-table>
  </v-main>
</template>

<script>
  import CalculatorService from '../common/calculator.service'

  export default {
    name: 'lease-view',
    data () {
      return {
        valid: false,
        amount: null,
        period: null,
        interest: null,
        initialPaymentPercentage: null,

        lease: null,
        payments: []
      }
    },
    methods: {
      submit () {
        CalculatorService.lease({
            amount: this.amount,
            period: this.period,
            interest: this.interest,
            initialPaymentPercentage: this.initialPaymentPercentage
        }).then((response) => {
            this.lease = response.data.message;
            this.payments = response.data.message.repaymentPlan;
        });
      }
    }
  }
</script>