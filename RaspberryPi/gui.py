from guizero import App, Text, PushButton, Box
import datetime as dt
from datetime import datetime
from enum import Enum
import math

app = App(bg="#ffffff")

secRatio=25/18

class Mode(Enum):
    Day=0
    Night=1

myMode=Mode.Day

def toggleNightMode():    
    global myMode
    if myMode is Mode.Day:
        myMode=Mode.Night
        nightModeBtn.image="moon.png"
        app.bg="#000000"
        time_msg.text_color="#ffffff"
    else:
        myMode=Mode.Day
        nightModeBtn.image="sun.png"
        app.bg="#ffffff"
        time_msg.text_color="#000000"

button_box = Box(app, width="fill", align="bottom")
nightModeBtn = PushButton(button_box, toggleNightMode, image="sun.png", align="right")

def tick():
    now = datetime.now()
    today=datetime.today()
    midnight=datetime.combine(today,datetime.min.time())
    start=dt.datetime(year=midnight.year,month=midnight.month,day=midnight.day,hour=6,minute=0,second=0,microsecond=0,tzinfo=None,fold=0)
    time_msg.value=str(start)
    metricSecsSinceStart=0
    delta= now-midnight
    if now>midnight and now < start:
        metricSecsSinceStart=90000 + (delta.total_seconds() * secRatio)
    else:
        standardSec = delta.total_seconds() - 21600
        metricSecsSinceStart=standardSec * secRatio

    currentMetricHours = math.floor(metricSecsSinceStart/10000)
    currentMetricMinutes = math.floor((metricSecsSinceStart-(currentMetricHours*10000))/100)
    currentMetricSeconds = math.floor(metricSecsSinceStart - (currentMetricHours*10000)-(currentMetricMinutes*100))
    if currentMetricHours==12:
        currentMetricHours=0
    time_msg.value= '{H}:{M:02}:{S:02} MT\n'.format(
        H=1+currentMetricHours,M=currentMetricMinutes,S=currentMetricSeconds) + now.strftime("%I:%M:%S %p")


time_msg= Text(app,text="",width='fill',height='fill',size=120)
time_msg.repeat(100,tick)
app.tk.attributes("-fullscreen",True)
app.display()