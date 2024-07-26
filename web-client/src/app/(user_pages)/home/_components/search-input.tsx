'use client'

import SearchCheckBox from '@/app/(user_pages)/home/_components/search-checkbox'
import { Input } from '@/components/ui/input'
import { Label } from '@/components/ui/label'
import { SearchBusinessQueryOption, SearchBusinessQueryOptionKeys } from '@/types/business.types'
import { useState } from 'react'
import { usePathname, useRouter, useSearchParams } from 'next/navigation'
import { IndustryResponse } from '@/types/industry.types'

type props = {
  searchOptions: SearchBusinessQueryOption
  industries: IndustryResponse[]
}

export default function SearchBusinessInput({ searchOptions, industries }: props) {
  const searchParams = useSearchParams()
  const pathname = usePathname()
  const router = useRouter()
  const industriesParam = searchOptions.industries?.split('-i-') ?? []
  const isVerifiedParam = searchOptions.isVerified?.split('-i-') ?? []
  const companySizeParam = searchOptions.company_size?.split('-i-') ?? []
  const numberYearOperationParam = searchOptions.number_year_operation?.split('-i-') ?? []

  const [searchValue, setSearchValue] = useState<string>(searchOptions.searchTerm ?? '')

  const handleSearchChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    handleSearch('searchTerm', event.target.value)
    setSearchValue(event.target.value)
  }

  const handleSearch = (name: SearchBusinessQueryOptionKeys, term: string) => {
    const params = new URLSearchParams(searchParams)
    if (term) {
      params.set(name, term)
    } else {
      params.delete(name)
    }
    router.push(`${pathname}?${params.toString()}`, { scroll: false })
  }

  const handleCheckBoxChange = (name: SearchBusinessQueryOptionKeys, term: string, isChecked: boolean) => {
    const params = new URLSearchParams(searchParams.toString())
    let currentValues = params.get(name)?.split('-i-') ?? []
    if (isChecked) {
      currentValues.push(term)
    } else {
      const index = currentValues.indexOf(term)
      if (index > -1) {
        currentValues.splice(index, 1)
      }
    }
    currentValues = [...new Set(currentValues)]
    const newValue = currentValues.join('-i-')
    handleSearch(name, newValue)
  }

  return (
    <>
      <Input placeholder='Tìm kiếm theo tên hoặc địa chỉ' value={searchValue} onChange={handleSearchChange} />
      <div className='mt-5'>
        <Label>Lĩnh vực doanh nghiệp</Label>
        <div className='ml-5 mt-2'>
          {industries.length > 0 ? (
            industries.map((industry) => (
              <SearchCheckBox
                value={industry.id}
                isChecked={industriesParam.includes(industry.id)}
                key={industry.id}
                id={`industries-${industry.id}`}
                display={industry.name}
                searchName='industries'
                handleCheckBoxChange={handleCheckBoxChange}
              />
            ))
          ) : (
            <></>
          )}
        </div>
      </div>
      <div className='mt-5'>
        <Label>Quy mô doanh nghiệp</Label>
        <div className='ml-5 mt-2'>
          <SearchCheckBox
            value={'0'}
            isChecked={companySizeParam.includes('0')}
            id='company_size-0'
            display='Dưới 50 nhân viên'
            searchName='company_size'
            handleCheckBoxChange={handleCheckBoxChange}
          />
          <SearchCheckBox
            value={'1'}
            isChecked={companySizeParam.includes('1')}
            id='company_size-1'
            display='Từ 50 nhân viên - 100 nhân viên'
            searchName='company_size'
            handleCheckBoxChange={handleCheckBoxChange}
          />
          <SearchCheckBox
            value={'2'}
            isChecked={companySizeParam.includes('2')}
            id='company_size-2'
            display='Từ 100 nhân viên - 200 nhân viên'
            searchName='company_size'
            handleCheckBoxChange={handleCheckBoxChange}
          />
          <SearchCheckBox
            value={'3'}
            isChecked={companySizeParam.includes('3')}
            id='company_size-3'
            display='Từ 200 nhân viên - 500 nhân viên'
            searchName='company_size'
            handleCheckBoxChange={handleCheckBoxChange}
          />
          <SearchCheckBox
            value={'4'}
            isChecked={companySizeParam.includes('4')}
            id='company_size-4'
            display='Từ 500 nhân viên - 1000 nhân viên'
            searchName='company_size'
            handleCheckBoxChange={handleCheckBoxChange}
          />
          <SearchCheckBox
            value={'5'}
            isChecked={companySizeParam.includes('5')}
            id='company_size-5'
            display='Trên 1000 nhân viên'
            searchName='company_size'
            handleCheckBoxChange={handleCheckBoxChange}
          />
        </div>
      </div>
      <div className='mt-5'>
        <Label>Trạng thái doanh nghiệp</Label>
        <div className='ml-5 mt-2'>
          <SearchCheckBox
            value={'true'}
            isChecked={isVerifiedParam.includes('true')}
            id='isVerified-true'
            display='Đã được xác thực'
            searchName='isVerified'
            handleCheckBoxChange={handleCheckBoxChange}
          />
          <SearchCheckBox
            value={'false'}
            isChecked={isVerifiedParam.includes('false')}
            id='isVerified-false'
            display='Chưa được xác thực'
            searchName='isVerified'
            handleCheckBoxChange={handleCheckBoxChange}
          />
        </div>
      </div>
      <div className='mt-5'>
        <Label>Số năm thành lập</Label>
        <div className='ml-5 mt-2'>
          <SearchCheckBox
            value={'0'}
            isChecked={numberYearOperationParam.includes('0')}
            id='number_year_operation-0'
            display='Dưới 1 năm'
            searchName='number_year_operation'
            handleCheckBoxChange={handleCheckBoxChange}
          />
          <SearchCheckBox
            value={'1'}
            isChecked={numberYearOperationParam.includes('1')}
            id='number_year_operation-1'
            display='Từ 2 năm - 5 năm'
            searchName='number_year_operation'
            handleCheckBoxChange={handleCheckBoxChange}
          />
          <SearchCheckBox
            value={'2'}
            isChecked={numberYearOperationParam.includes('2')}
            id='number_year_operation-2'
            display='Từ 5 năm - 10 năm'
            searchName='number_year_operation'
            handleCheckBoxChange={handleCheckBoxChange}
          />
          <SearchCheckBox
            value={'3'}
            isChecked={numberYearOperationParam.includes('3')}
            id='number_year_operation-3'
            display='Từ 10 năm - 20 năm'
            searchName='number_year_operation'
            handleCheckBoxChange={handleCheckBoxChange}
          />
          <SearchCheckBox
            value={'4'}
            isChecked={numberYearOperationParam.includes('4')}
            id='number_year_operation-4'
            display='Trên 20 năm'
            searchName='number_year_operation'
            handleCheckBoxChange={handleCheckBoxChange}
          />
        </div>
      </div>
    </>
  )
}
