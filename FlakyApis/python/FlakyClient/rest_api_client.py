import asyncio
from datetime import datetime

import aiohttp
import time
import numpy as np
import matplotlib.pyplot as plt
from collections import namedtuple
from aiohttp import ClientSession
import pandas as pd

FlakyApiRequest = namedtuple('FlakyApiRequest',
                             ['request_id', 'request_start_at', 'request_finish_at', 'response_type'])

CLOSE, HLAFOPEN, OPEN = 1, 2, 3


def plot_data(res_array):
    fig, ax = plt.subplots(figsize=(11, 5))
    ax.plot(res_array[0], label='close', color='green')
    ax.plot(res_array[1], label='half open', color='red')
    ax.plot(res_array[2], label='open', color='purple')
    plt.show()


async def fetch(url, request_id, session) -> FlakyApiRequest:
    request_start_at = time.time()
    async with session.get(url, ssl=False) as response:
        # print(f'requesting ({request_id})')
        # text = await response.read()
        if response.status == 200:
            response_type = 'CLOSE'
        elif response.status == 503:
            response_type = 'HLAFOPEN'
        elif response.status == 406:
            response_type = 'OPEN'
        # print(f'requested ({request_id}):{response_type}')
        flaky_request = FlakyApiRequest(request_id, request_start_at, time.time(), response_type)

        return flaky_request


async def main(number_of_request=50):
    url = "https://localhost:44343/Weather?city=tehran"
    tasks = []
    conn = aiohttp.TCPConnector()
    async with ClientSession(connector=conn) as session:
        for i in range(number_of_request):
            task = asyncio.ensure_future(fetch(url.format(i), i, session))
            tasks.append(task)

        await asyncio.gather(*tasks)

    response_dict = {'response_type': [], 'request_start_at': [], 'request_finish_at': []}
    for i, task in enumerate(tasks):
        result = task.result()
        response_dict['response_type'].append(result.response_type)
        response_dict['request_start_at'].append(datetime.fromtimestamp(result.request_start_at))
        response_dict['request_finish_at'].append(datetime.fromtimestamp(result.request_finish_at))
    df = pd.DataFrame(response_dict)
    df['request_start_at'] = pd.to_datetime(df["request_start_at"])
    df['request_finish_at'] = pd.to_datetime(df["request_finish_at"])

    df.groupby(df['response_type']).count().plot()
    plt.show()
    print(df)
    print('finish')


if __name__ == '__main__':
    loop = asyncio.get_event_loop()
    future = asyncio.ensure_future(main())
    loop.run_until_complete(future)
