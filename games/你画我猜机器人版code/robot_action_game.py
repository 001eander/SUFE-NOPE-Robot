#!/usr/bin/env python3
# encoding: utf-8
import sys

sys.path.append('/home/pi/TonyPi/')
import HiwonderSDK.Board as Board
import HiwonderSDK.ActionGroupControl as AGC
import random

action_dict = {
    # '0': "Chinese name",

    # 单个动作
    "大鹏展翅": '0',  # 双臂伸展
    #"金鸡独立": 'back_end',  # 左抬脚后放下    1
    "逃之夭夭": 'back_fast',  # 快速后退
    "暂避锋芒": 'back_one_step',  # 后退一步
    "摩拳擦掌": 'wing_chun',  # 咏春
    "起起落落": 'sit_ups',  # 仰卧起坐
    #"五体投地": 'push_ups',  # 俯卧撑
    "致命挑逗": 'wave',  # 挥手
    "私密马赛": 'bow',  # 鞠躬
    "神采飞扬": 'chest',  # 开怀大笑
    "大步流星": 'go_forward_fast',  # 快速前进
    #"步履蹒跚": 'go_forward_slow',  # 缓慢前进
    "踏步不前": 'stepping',  # 原地踏步
    "大力抽射": 'left_shot',  # 左脚射门
    #"东山再起": 'stand_up_back',  # 后跌倒起立

    # 动作组合
    "退避三舍": ['back_one_step']*3,  # 后退三步：'back_one_step'*3
    # 先快速左移：'left_move_fast'，再快速右移：'right_move_fast'
    "横行霸道": ['left_move_fast', 'right_move_fast'],
    # 鞠躬：'bow'，左勾拳：'left_uppercut'，右勾拳：'right_uppercut'
    "先礼后兵": ['bow', 'left_uppercut', 'right_uppercut'],
    "前倨后恭": ['chest', 'bow'],  # 开怀大笑：'chest'，鞠躬：'bow'
    "心灰意冷": ['go_forward', 'squat'],  # 缓慢前进：'go_forward_slow'，下蹲：'squat'
}

action_list = list(action_dict.keys())


def run_actions(actions_todo):
    if type(actions_todo) == list:
        for o in actions_todo:
            AGC.runActionGroup(o)
    elif type(actions_todo) == str:
        AGC.runActionGroup(actions_todo)


if __name__ == "__main__":
    count = len(action_list)
    while True:
        query = input("input 's' to start # ")
        if query == 's':
            answer = random.choice(action_list)
            print("\n\n#######  Question  ##########")
            print("这个动作是什么呢?")
            # AGC.runActionGroup(action_list[random_idx])
            run_actions(action_dict[answer])
            _action_list = action_list.copy()
            _action_list.remove(answer)
            answers = random.sample(_action_list, 3)
            answers.append(answer)
            answers = random.sample(answers, 4)
            for k, a in zip(['A.', 'B.', 'C.', 'D.'], answers):
                print(k, a)
            input()
            print("-------------------")
            print("答案是:", answer)
            AGC.runActionGroup("stand")
        if query == 'q':
            exit(0)
