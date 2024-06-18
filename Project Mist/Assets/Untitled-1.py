class Leetcode1(object):
    def searchInsert(self, nums, target):
        """
        :type nums: List[int]
        :type target: int
        :rtype: int
        """
        self.searchInsertRecurse(nums,target,0)

    def searchInsertRecurse(self, nums, target, index):
        if nums[0] == target:
            return index
        if(nums[len(nums)/2] < target):
            return self.searchInsertRecurse(nums[:len(nums)/2], target, index + nums.length/2)
        if(nums[len(nums)/2 > target]):
            return self.searchInsertRecurse(nums[len(nums)/2:], target, index+0)

        