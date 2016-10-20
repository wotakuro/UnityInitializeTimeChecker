#include <mach/mach.h>

#define GET_FLOAT_SEC_TIME(tmVal) static_cast<float>( tmVal.seconds) + (tmVal.microseconds * 0.000001f);

extern "C"{
    float InitializeTimeCheckerGetCpuSecFromAppBoot(){
        float userTime = 0.0f;
        float sysTime = 0.0f;
        struct task_basic_info t_info;
        mach_msg_type_number_t t_info_count = TASK_BASIC_INFO_COUNT;

        if (task_info(current_task(), TASK_BASIC_INFO, (task_info_t)&t_info, &t_info_count)== KERN_SUCCESS)
        {
            userTime += GET_FLOAT_SEC_TIME( t_info.user_time);
            sysTime += GET_FLOAT_SEC_TIME( t_info.system_time);
        }
        
        //
        // 実行中のスレッドのCPU使用時間(TASK_THREAD_TIMES_INFO_COUNT)を取得する。
        //
        
        struct task_thread_times_info tti;
        t_info_count = TASK_THREAD_TIMES_INFO_COUNT;
        
        kern_return_t status = task_info(current_task(), TASK_THREAD_TIMES_INFO,
                                         (task_info_t)&tti, &t_info_count);
        if (status == KERN_SUCCESS) {
            userTime += GET_FLOAT_SEC_TIME( tti.user_time);
            sysTime += GET_FLOAT_SEC_TIME( tti.system_time);
        }
        return userTime + sysTime;
    }
}


