import numpy as np
from FlakyEnvironment import FlakyEnvironment, CIRCUIT_CLOSED, CIRCUIT_HALFOPEN, CIRCUIT_OPEN, REQUEST_SUCCEED, \
    REQUEST_FAILED

statuses = {CIRCUIT_CLOSED: 'closed', CIRCUIT_HALFOPEN: 'half-open', CIRCUIT_OPEN: 'open'}
responses = {REQUEST_SUCCEED: 'succeed', REQUEST_FAILED: 'failed'}
env = FlakyEnvironment(threshold=3, timeout=10)
env.reset()
total_time_step = 1000
state = 3
for step in range(total_time_step):
    next_state, reward, is_done, info = env.step(np.random.choice([1, 2, 3]))
    print(f"{step}\t{statuses[state]}\t {responses[info['request']] if state != 1 else '-' }\t {statuses[next_state]}\t")
    state = next_state

#
# import numpy as np
# import matplotlib.pyplot as plt
#
# plt.plot([round(np.random.exponential(0.15)) for _ in range(500)])
#
# plt.show()
