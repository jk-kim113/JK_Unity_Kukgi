using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm_Test
{
    class Program
    {
        static void Main(string[] args)
        {
            PMatrix mat = new PMatrix();
            PVector v1 = new PVector(1, 0, 3);
            PVector v2 = v1 * mat;

            // ======================================================

            Node<string> node = new Node<string>("first");
            PLinkedList<string> Llist = new PLinkedList<string>();
            Llist.AddFirst(node);
        }
    }
}

/*
 * 랜더링 파이프 라인 과정
 * 
 * 1. 로컬 스페이스
 *      => 물체의 버텍스를 정의하는 데 이용하는 좌표 시스템
 * 2. 월드 스페이스
 *      => 로컬 스페이스를 가진 각각의 오브젝트를 월드상의 표현
 * 3. 뷰 스페이스
 *      => 월드 스페이스에서 표현된 오브젝트를 카메라를 기준으로한 좌표로 변환
 * 4. 백 스페이스 컬링
 *      => 카메라에 보여지지 않는 후면을 처리
 * 5. 조명
 *      => 물체에 명암
 * 6. 클리핑
 *      => 카메라 시야에서 보이지 않는 부분을 처리
 * 7. 투영
 *      => 3D 상의 표현된 오브젝트를 카메라로 투영
 * 8. 뷰 포트
 *      => 프로젝트상의 좌표를 게임상에 표현되는 뷰 포트 화면으로 변환
 * 9. 래스터 라이즈
 *      => 각각의 폴리곤을 출력하는데 필요한 픽셀컬러를 계산하는 과정
 */
