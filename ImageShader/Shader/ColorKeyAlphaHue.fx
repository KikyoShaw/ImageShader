sampler2D input1 : register(S0);        // 替换透明层并压缩后的图片
sampler2D input2 : register(S1);        // 选择性输入的背景层
                                        // 用于展示带有颜色的背景层是如何通过shader编程后作为透明层

float4 ColorKey : register(C0);         // 替换透明层的背景色
float ToleranceBack : register(C1);     // ColorKey和输入图像的容差
float ToleranceEdge1 : register(C2);    // 表示ColorKey最大的颜色值与余下两个颜色值之差的容差
float ToleranceEdge2 : register(C3);    // 表示ColorKey最大的颜色值的两个颜色值之差的容差
float MaxColor : register(C4);          // 知识ColorKey中最大的颜色值是哪一个


float4 main(float2 uv : TEXCOORD) : COLOR
{
    // 读取输入图像当前像素的颜色值
    float4 color = tex2D(input1, uv);
    float4 color2 = tex2D(input2, uv);

    // 如果ColorKey的颜色和压缩图像的颜色小于背景容差
    // 则认为这一层是完全透明的，返回背景图像的颜色
    if (all(abs(color.rgb - ColorKey.rgb) < ToleranceBack)) {
        color.rgba = 0;
        return color2;
    }

    // 如果ColorKey的最大颜色是红色
    if (abs(MaxColor - 1) < 0.000001) {
        // 则计算红色和其余两色差值，以及两个颜色之间的差值
        // 判断是否符合容差值
        if (((color.r - color.g) + (color.r - color.b)) / 2 > ToleranceEdge1
            && (abs(color.g - color.b)) < ToleranceEdge2)
        {
            // 尝试恢复原始的透明度，原始数据已经丢失难以计算，采用经验值进行恢复
            color.a = (1 - color.r) / 2 + (1 - color.g) / 4 + (1 - color.b) / 4;
            // 尝试恢复原始颜色的灰度值
            color.r = color.r * 0.299 + color.g * 0.587 + color.b * 0.114;
            color.g = color.r;
            color.b = color.r;
            // 融合背景的颜色值
            color.rgb = color.rgb * color.a + (1 - color.a) * color2.rgb;
            color.a = 1;
        }
    }
    else if (abs(MaxColor - 2) < 0.000001) {
        if (((color.g - color.r) + (color.g - color.b)) / 2 > ToleranceEdge1
            && (abs(color.r - color.b)) < ToleranceEdge2)
        {
            color.a = (1 - color.g) / 2 + (1 - color.r) / 4 + (1 - color.b) / 4;
            color.g = color.r * 0.299 + color.g * 0.587 + color.b * 0.114;
            color.r = color.g;
            color.b = color.g;
            color.rgb = color.rgb * color.a + (1 - color.a) * color2.rgb;
            color.a = 1;
        }
    }
    else if (abs(MaxColor - 3) < 0.000001) {
        if (((color.b - color.b) + (color.b - color.r)) / 2 > ToleranceEdge1
            && (abs(color.g - color.r)) < ToleranceEdge2)
        {
            color.a = (1 - color.b) / 2 + (1 - color.r) / 4 + (1 - color.g) / 4;
            color.b = color.r * 0.299 + color.g * 0.587 + color.b * 0.114;
            color.r = color.b;
            color.g = color.b;
            color.rgb = color.rgb * color.a + (1 - color.a) * color2.rgb;
            color.a = 1;
        }
    }
    return color;
}
