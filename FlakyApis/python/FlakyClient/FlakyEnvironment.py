import numpy as np

CIRCUIT_OPEN, CIRCUIT_HALFOPEN, CIRCUIT_CLOSED = 1, 2, 3
REQUEST_SUCCEED, REQUEST_FAILED = 1, 2


class FlakyEnvironment:
    def __init__(self, threshold, timeout, total_step=1000):
        """
        this is a specific environment that expose a flaky behavior in order to test circuit breaker pattern.

        :param threshold: once the number of failures exceeds threshold,
        the environment trips and change the status to open.
        the value should be between 0 to 1.

        :param timeout: circuit breaker ramin in open status until timeout elapsed.
        the value should be in second
        """
        self._threshold = threshold
        self._timeout = timeout

        self._status = CIRCUIT_CLOSED
        self._failed_requests_count = 0
        self._last_change_step = 0
        self._step = 0
        self._total_step = total_step
        self._failure_events = [step for step in range(self._total_step) if round(np.random.exponential(0.35)) == 1]
        print(self._failure_events)

    @property
    def status(self):
        return self._status

    def reset(self):
        self._failed_requests_count = 0
        self._last_change_step = self._step

    def step(self, selected_status):
        reward = -1
        if selected_status == self._status:
            reward = 0
        request_result = REQUEST_SUCCEED if self.is_sys_up() else REQUEST_FAILED
        self.on_requesting(request_result)
        return self._status, reward, False, {'request': request_result}

    def on_requesting(self, request):
        self._step += 1
        if request == REQUEST_FAILED:
            self._failed_requests_count += 1
        if self._step - self._last_change_step > self._timeout:
            if self._status == CIRCUIT_OPEN:
                self._status = CIRCUIT_HALFOPEN
            self.reset()
            print('---------------')
        else:
            if self._status == CIRCUIT_CLOSED:
                if self._failed_requests_count >= self._threshold:
                    self._status = CIRCUIT_OPEN
                    self.reset()
            elif self._status == CIRCUIT_HALFOPEN:
                if request == REQUEST_SUCCEED:
                    self._status = CIRCUIT_CLOSED
                    self.reset()
                elif request == REQUEST_FAILED:
                    self._status = CIRCUIT_OPEN

    def is_sys_up(self):
        return not self._step % self._total_step in self._failure_events
