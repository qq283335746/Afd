import android.Manifest;
import android.annotation.TargetApi;
import android.app.Activity;
import android.content.pm.PackageManager;
import android.os.Build;

import java.util.ArrayList;

/**
 * Created by lizhipeng on 2016/10/20.
 */

public class AndroidPermissonUtils {
    //注意，这里没有final
    private static AndroidPermissonUtils single = null;
    public static final int SDK_PERMISSION_REQUEST = 127;
    private String permissionInfo;
    private Activity activity;

    //私有的默认构造子
    private AndroidPermissonUtils() {
    }

    //静态工厂方法
    public synchronized static AndroidPermissonUtils getInstance() {
        if (single == null) {
            single = new AndroidPermissonUtils();
        }
        return single;
    }
    @TargetApi(23)
    public void getPersimmions(Activity activity) {
        this.activity = activity;
        if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.M) {
            ArrayList<String> permissions = new ArrayList<String>();
            /***
             * 定位权限为必须权限，用户如果禁止，则每次进入都会申请
             */
            // 定位精确位置
            if (activity.checkSelfPermission(Manifest.permission.ACCESS_FINE_LOCATION) != PackageManager.PERMISSION_GRANTED) {
                permissions.add(Manifest.permission.ACCESS_FINE_LOCATION);
            }
            if (activity.checkSelfPermission(Manifest.permission.ACCESS_COARSE_LOCATION) != PackageManager.PERMISSION_GRANTED) {
                permissions.add(Manifest.permission.ACCESS_COARSE_LOCATION);
            }
            if (permissions.size() > 0) {
                activity.requestPermissions(permissions.toArray(new String[permissions.size()]), SDK_PERMISSION_REQUEST);
            }
        }
    }


}
